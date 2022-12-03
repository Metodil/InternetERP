namespace InternetERP.Web.CustomValidate
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ArrayIsInt : ValidationAttribute
    {
        public override bool IsValid(object val)
        {
            try
            {
                int[] arrayInteger = (int[])val;
                foreach (var num in arrayInteger)
                {
                    if (num < 0)
                    {
                        return false;
                    }
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}
