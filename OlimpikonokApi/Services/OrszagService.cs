using OlimpikonokApi.Models;

namespace OlimpikonokApi.Services
{
    public class OrszagService
    {
        public List<Orszag> GetAll()
        {
            try
            {
                List<Orszag> eredemny = new OlimpikonokContext().Orszags.ToList();
                return eredemny;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
             

        }
    }
}
