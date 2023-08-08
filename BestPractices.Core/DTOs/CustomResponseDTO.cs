using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestPractices.Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        [JsonIgnore] //kod içerisinde lazım jsona donerken kod ıcerısnde ıgnıoore et dıyoruz
        public int StatusCode { get; set; }

        public static CustomResponseDTO<T> Success(int statusCode, T data) //statıc factory method bunlar(newlemeden)
        {
            return new CustomResponseDTO<T> { Data = data, StatusCode = statusCode};
        }

        public static CustomResponseDTO<T> Success(int statusCode) //statıc factory method bunlar(newlemeden)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode};
        }
        public static CustomResponseDTO<T> Fail(int statusCode,List<string> errors) //statıc factory method bunlar(newlemeden)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode ,Errors=errors};
        }


        public static CustomResponseDTO<T> Fail(int statusCode,string errors) //statıc factory method bunlar(newlemeden)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = new List<string>{errors }};
        }
    }
}
