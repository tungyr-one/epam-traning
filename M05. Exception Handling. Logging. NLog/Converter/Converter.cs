using System;
using Microsoft.Extensions.Logging;
namespace Converter
{
    public class StrConverter
    {
        private readonly ILogger<StrConverter> _logger;

        public StrConverter(ILogger<StrConverter> logger)
        {
            _logger = logger;
        }

        public int ConvertStrToInt(string number)
        {
            _logger.LogInformation($"Start convert string \"{number}\"");
            int result = 0;

            for (int i = 0; i < number.Length; i++)
                if (!Char.IsLetter(number[i]))
                {
                    { result = result * 10 + ((int)number[i] - 48); }
                }
                else
                {
                    _logger.LogError($"Letter \"{number[i]}\" found in string! ");
                }


            _logger.LogInformation($"Convertion completed!");
            return result;
        }
    }
}
