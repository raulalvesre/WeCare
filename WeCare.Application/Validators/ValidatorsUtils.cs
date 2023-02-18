using System.Text.RegularExpressions;

namespace WeCare.Application.Validators;

public class ValidatorsUtils
{
    private static readonly string TelephoneRegex = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$";
    private static readonly string CpfRegex = @"/^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}$/";
    private static readonly string CnpjRegex = @"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})";

    public static bool IsInvalidTelephone(string telephone)
    {
        return !Regex.Match(telephone, TelephoneRegex).Success;
    }
    
    public static bool IsInvalidCpf(string cpf)
    {
        return !Regex.Match(cpf, CpfRegex).Success;
    }
    
    public static bool IsNotOfAdultAge(DateTime birthDate)
    {
        return birthDate.AddYears(18) > DateTime.Today;
    }

    public static bool IsInvalidCnpj(string cnpj)
    {
        return !Regex.Match(cnpj, CnpjRegex).Success;
    }
}