using Microsoft.EntityFrameworkCore;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Common;

namespace pvo_dictionary_api.Seeder
{
    class UserSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public UserSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    user_id = 1,
                    user_name = "Test",
                    password = "Test",
                    email = "Test@gmail.com",
                    display_name = "Test",
                    full_name = "Test",
                    birthday = new DateTime(2001, 05, 14),
                    position = "",
                    avatar = "",
                    status = 1,
                }
                );
        }
    }
}
