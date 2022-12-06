using System;

namespace qodeless.presentation.UI.Web.Extensions
{
    public static class ValidationsExtension 
    {
         /// <summary>
        /// Valida o numero do cartão sus, deve conter 15 caracteres
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static bool IsValidCNS(this string str)
        {
            return str.Length == 15;
        }

        /// <summary>
        /// Realiza a validação do CNPJ
        /// </summary>

        public static bool IsValidCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }


        /// <summary>
        /// Valida cpf
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        

        public static bool IsValidRG(this string RG)
        {
            try
            {
                // Pegar os 8 primeiros dígitos e multiplicar o primeiro por 2, e cada número subir + 1 até chegar em 9
                RG = RG.Replace(".", "").Replace("-", "").Replace(",", "").Replace(" ", "").Trim();

                int n1 = int.Parse(RG.Substring(0, 1));
                int n2 = int.Parse(RG.Substring(1, 1));
                int n3 = int.Parse(RG.Substring(2, 1));
                int n4 = int.Parse(RG.Substring(3, 1));
                int n5 = int.Parse(RG.Substring(4, 1));
                int n6 = int.Parse(RG.Substring(5, 1));
                int n7 = int.Parse(RG.Substring(6, 1));
                int n8 = int.Parse(RG.Substring(7, 1));

                string DV = RG.Substring(8, 1);

                int Soma = n1 * 2 + n2 * 3 + n3 * 4 + n4 * 5 + n5 * 6 + n6 * 7 + n7 * 8 + n8 * 9;

                string digitoVerificador = Convert.ToString(Soma % 11);

                // Se o dígito verificador for igual a 1, automaticamente ele se tornará o algarismo romano X,
                // pois será feito o cálculo de 11 - o dígito verificador. No caso, ficaria 11 - 1, que é igual a 10.

                if (digitoVerificador == "1")
                {
                    digitoVerificador = "X";
                }

                // Se o dígito verificador for igual a 0, automaticamente ele se torna 0, pois 11 - 0 é igual a 11, e
                // não será permitido isso no dígito verificador, então automaticamente o dígito será 0.
                else if (digitoVerificador == "0")
                {
                    digitoVerificador = "0";
                }

                // Se não for nem 0 nem 1, vai ser feito 11 - o dígito verificador. Por exemplo, se o a soma dividida por 11
                // deu resto 5, será feito 11 - 5, que é igual a 6. Então automaticamente o dígito verificador do RG se tornará o número 6!

                else
                {
                    digitoVerificador = (11 - int.Parse(digitoVerificador)).ToString();
                }

                // Verificar se o dígito verificador é igual ao DV do RG que está sendo validado.

                if (digitoVerificador == DV)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Error");
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

       
        
    }
}

    


