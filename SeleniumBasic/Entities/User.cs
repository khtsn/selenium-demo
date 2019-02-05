using SeleniumBasic.Utilities;
using System;

namespace SeleniumBasic.Entities
{
    public class User
    {
        private string email;
        private string password;
        private string passwordConfirm;
        private string pid;
        private string username;

        private string key = "khanh";

        public string Email
        {
            get
            {
                return String.Format("{0}{1}@mailinator.com", key, username); ;
            }

            set
            {
                email = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string PasswordConfirm
        {
            get
            {
                return passwordConfirm;
            }

            set
            {
                passwordConfirm = value;
            }
        }

        public string Pid
        {
            get
            {
                return pid;
            }

            set
            {
                pid = value;
            }
        }

        public string Username
        {
            get
            {
                return key + username;
            }

            set
            {
                username = value;
            }
        }

        public void InitData()
        {
            username = StringUtils.RandomString(8, true);
            password = StringUtils.RandomString(8, true);
            passwordConfirm = StringUtils.RandomString(8, true);
            pid = StringUtils.RandomString(8, true);
        }
    }
}
