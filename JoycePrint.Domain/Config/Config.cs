using System;

namespace JoycePrint.Domain.Config
{
    public class Config : IConfig
    {
        public string RecaptchaUrl
        {
            get
            {
                return "https://www.google.com/recaptcha/api/siteverify";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string RecaptchaSecretKey
        {
            get
            {
                return "6LcC2Q0UAAAAALvPAkBtQT2a5AE8DUCotVfQu04t";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string RecaptchaPublicKey
        {
            get
            {
                return "6LcC2Q0UAAAAADtadrrG_FTRs82tvd2J1fOwK-KW";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
