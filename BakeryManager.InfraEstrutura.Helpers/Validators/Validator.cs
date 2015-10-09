using System;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace AddTech.Infraestrutura.Helpers.Validators
{
    public static class Validator
    {
        public static bool ValidaCNPJ(string cnpj)
        {
            var multiplicador1 = new int[12] {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new int[13] {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;
            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString())*multiplicador1[i];
            var resto = (soma%11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString())*multiplicador2[i];
            resto = (soma%11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cnpj.EndsWith(digito);
        }

        public static bool ValidaCPF(string cpf)
        {
            var multiplicador1 = new int[9] {10, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new int[10] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2};
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
                soma += int.Parse(tempCpf[i].ToString())*multiplicador1[i];
            resto = soma%11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString())*multiplicador2[i];
            resto = soma%11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }

        public static bool ValidaPIS(string pis)
        {
            var multiplicador = new int[10] {3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            int soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString())*multiplicador[i];
            int resto = soma%11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }

        public static bool ValidaEnderecoSite(string EnderecoSite, bool AdmitirValorNulo = true)
        {
            
            if (!AdmitirValorNulo)
                return !string.IsNullOrEmpty(EnderecoSite) && Regex.IsMatch(EnderecoSite, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?");

            return true;

        }

        public static bool ValidateEmail(string Email, bool AdmitirValorNulo = true)
        {

            if (!AdmitirValorNulo)
                return !string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            
            return true;

        }

        
    }
}