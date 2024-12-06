namespace Bulky_Web.helpers
{
    public class ArrangeQueryIncludeTypes
    {
        public string ArrangeQueryInclude(List<string> includeProperties)
        {
            string includeString = "";
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    includeString += includeProperty + ",";
                }
            }
            return includeString;
        }
    }
}
