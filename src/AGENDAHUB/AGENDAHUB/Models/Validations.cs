using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AGENDAHUB.Models
{
    public class Validations
    {
        public class CPFAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var cpf = value as string;

                if (string.IsNullOrWhiteSpace(cpf))
                {
                    return ValidationResult.Success; // CPF vazio é válido
                }

                // Remove caracteres não numéricos do CPF
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                if (cpf.Length != 11)
                {
                    return new ValidationResult("CPF deve conter 11 dígitos.");
                }

                // Verifica se todos os dígitos são iguais (CPF inválido)
                if (cpf.Distinct().Count() == 1)
                {
                    return new ValidationResult("CPF inválido.");
                }

                // Calcula os dígitos verificadores
                int soma = 0;
                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(cpf[i].ToString()) * (10 - i);
                }

                int primeiroDigitoVerificador = (soma * 10) % 11;

                if (primeiroDigitoVerificador == 10)
                {
                    primeiroDigitoVerificador = 0;
                }

                if (primeiroDigitoVerificador != int.Parse(cpf[9].ToString()))
                {
                    return new ValidationResult("CPF inválido.");
                }

                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(cpf[i].ToString()) * (11 - i);
                }

                int segundoDigitoVerificador = (soma * 10) % 11;

                if (segundoDigitoVerificador == 10)
                {
                    segundoDigitoVerificador = 0;
                }

                if (segundoDigitoVerificador != int.Parse(cpf[10].ToString()))
                {
                    return new ValidationResult("CPF inválido.");
                }

                return ValidationResult.Success; // CPF válido
            }
        }

        public class TelefoneAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var telefone = value as string;

                if (string.IsNullOrWhiteSpace(telefone))
                {
                    return ValidationResult.Success; // Telefone vazio é válido
                }

                // Remove caracteres não numéricos do telefone
                telefone = new string(telefone.Where(char.IsDigit).ToArray());

                // Verifica se o telefone tem pelo menos 10 dígitos
                if (telefone.Length < 10)
                {
                    return new ValidationResult("Número de telefone deve ter pelo menos 10 dígitos.");
                }

                return ValidationResult.Success; // Telefone válido
            }
        }

    }
}
