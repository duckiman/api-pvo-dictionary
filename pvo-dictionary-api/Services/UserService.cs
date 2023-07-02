using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Repositories;
using pvo_dictionary_api.Request;

namespace pvo_dictionary_api.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly AuditLogRepository _auditLogRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public UserService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _auditLogRepository = new AuditLogRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="updatePasswordRequest"></param>
        /// <returns></returns>
        public bool UpdatePassword(int userId, UpdatePasswordRequest updatePasswordRequest)
        {
            try
            {
                var user = _userRepository.FindOrFail(userId);
                if(user == null)
                {
                    throw new Exception("User does not exist in DB!");
                }
                if(user.password != Untill.CreateMD5(updatePasswordRequest.OldPassword))
                {
                    return false;
                }
                user.password = Untill.CreateMD5(updatePasswordRequest.NewPassword);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get logs
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="limit"></param>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        public object GetLogs(int userId, DateTime dateFrom, DateTime dateTo, int pageIndex, int limit, string? searchFilter)
        {
            try
            {
                var query = _auditLogRepository.FindAll().Where(row => row.user_id == userId && row.created_date >= dateFrom && row.created_date <= dateTo);
                if(!string.IsNullOrEmpty(searchFilter))
                {
                    query = query.Where(row => row.screen_info.Contains(searchFilter) || row.reference.Contains(searchFilter) || row.description.Contains(searchFilter));
                }

                var total = query.Count();
                int tmpByInt = total / limit;
                double tmpByDouble = (double)total / (double)limit;
                int totalPage = 1;
                if (tmpByDouble > (double)tmpByInt)
                {
                    totalPage = tmpByInt + 1;
                }
                else
                {
                    totalPage = tmpByInt;
                }
                query = query.Skip((pageIndex - 1) * limit).Take(limit);
                var amount = query.Count();
                return new
                {
                    data = query.ToList(),
                    Amount = amount,
                    PageSize = limit,
                    Total = total,
                    TotalPage = totalPage,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User UpdateUserInfor(int userId, UpdateUserInforRequest updateUserInforRequest)
        {
            try
            {
                var user = _userRepository.FindOrFail(userId);
                if (user == null)
                {
                    throw new Exception("User does not exist in DB!");
                }
                user.full_name = updateUserInforRequest.FullName;
                user.display_name = updateUserInforRequest.DisplayName;
                user.birthday = updateUserInforRequest.Birthday;
                user.position = updateUserInforRequest.Position;

                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save audit log
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object SaveLog(int userId, SaveLogRequest request)
        {
            try
            {
                var newAuditLog = new AuditLog()
                {
                    user_id = userId,
                    screen_info = request.ScreenInfo,
                    description = request.Description
                };

                if (!string.IsNullOrEmpty(request.Reference))
                {
                    newAuditLog.reference = request.Reference;
                }

                _auditLogRepository.Create(newAuditLog);
                _auditLogRepository.SaveChange();
                return newAuditLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
