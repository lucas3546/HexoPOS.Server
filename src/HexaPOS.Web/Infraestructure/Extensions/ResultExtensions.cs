using HexaPOS.Application.Common.Models;

namespace HexaPOS.Web.Infraestructure.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult<T>(
            this Result<T> result)
        {
            if (!result.Success)
            {
                return TypedResults.Problem(detail: string.Join("; ", result.Errors), statusCode: StatusCodes.Status400BadRequest, title: "Error");
            }

            return TypedResults.Ok(result.Value);
        }

        public static IResult ToHttpResult(
            this Result result)
        {
            if (!result.Success)
            {
                return TypedResults.Problem(detail: string.Join("; ", result.Errors), statusCode: StatusCodes.Status400BadRequest, title: "Error");
            }

            return TypedResults.Ok();
        }
    }
}
