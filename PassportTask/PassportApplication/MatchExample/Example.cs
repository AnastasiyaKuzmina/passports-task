using PassportApplication.MatchExample.ExampleClasses;
using PassportApplication.Results.Generic;

namespace PassportApplication.MatchExample
{
    /// <summary>
    /// Match example class
    /// </summary>
    public class Example
    {
        private static int Count { get; set; } = 1;
        public string FilePath { get; set; } = "some path";

        public Result<string> FirstFunction()
        {
            Random r = new Random();
            bool condition = r.Next(2) == 1 ? true : false;
            return condition ? Result<string>.Ok("ordinary string :)") : Result<string>.Fail("unlucky :(");
        }

        public Result<Class1> SecondFunction() 
        { 
            return FirstFunction().Match(
                str => Result<Class1>.Ok(new Class1 { Id = Count++, SomeString = str }),
                err => Result<Class1>.Fail(err.Message));
        }

        public async Task<Class2> ThirdFunction()
        {
            using (StreamWriter sw = new StreamWriter(FilePath)) 
            {
                return await SecondFunction().MatchAsync(
                    async class1 =>
                    { 
                        await sw.WriteAsync(class1.Id + "," + class1.SomeString);
                        return new Class2 { Id = Count++, Class1 = class1 };
                    },
                    async error =>
                    {
                        await sw.WriteAsync(error.ToString());
                        return new Class2 { Id = Count++, Class1 = null };
                    });
            }
        }
    }
}
