﻿namespace Application.BaseCommand
{
    public class ResponseResult
    {
        public object Data { get; set; }

        private ResponseResult()
        {

        }

        public static ResponseResult Instance(object data = null)
        {
            return new ResponseResult() { Data = data };
        }
    }
}
