using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tuya.Transversal.ResponseModel
{
    public class InvalidModel
    {
        private readonly static JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        public static void Response(ValidationResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            var error = new MessageResponse()
            {
                Status = 400,
                Message = "Parametros de Petición invalidos",
                Errors = GetErrors(result)
            };

            throw new BadRequestException(JsonSerializer.Serialize(error, jsonSerializerOptions));
        }

        private static List<ErrorDtl> GetErrors(ValidationResult result)
        {
            return [.. result.Errors
                .Select(x => new ErrorDtl(x.ErrorCode, x.ErrorMessage, x.PropertyName)
                {
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                    Path = x.PropertyName
                })];
        }
    }
}
