using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class TokenViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DtExpiration { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }

        public TokenViewModel(string phone)
        {
            Init();
            Phone = phone;
        }

        private void Init()
        {
            const int MINUTES_TO_EXPIRE = 3;
            const int TOKEN_MAX_DIGITS = 6;

            for (int i = 0; i < TOKEN_MAX_DIGITS; ++i)
                Code += new Random().Next(0, 9).ToString();

            DtExpiration = DateTime.Now.AddMinutes(MINUTES_TO_EXPIRE);
        }
    }
}
