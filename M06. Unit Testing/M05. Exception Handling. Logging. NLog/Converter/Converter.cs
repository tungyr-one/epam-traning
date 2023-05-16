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
            var result = 0;

            for (int i = 0; i < number.Length; i++)
                if (Char.IsDigit(number[i]))
                {
                    { result = result * 10 + ((int)number[i] - 48); }
                }
                else
                {
                    _logger.LogError($"Invalid number \"{number[i]}\" found in string! ");
                    _logger.LogInformation($"Convertion fails!");
                    return 0;
                }

            _logger.LogInformation($"Convertion completed succesfully!");
            return result;
        }
    }
}
