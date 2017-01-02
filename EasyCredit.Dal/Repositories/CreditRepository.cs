using EasyCredit.DAL.Context;
using EasyCredit.DAL.Models;


namespace EasyCredit.DAL.Repositories
{
    public class CreditRepository: BaseEasyCreditRepository<Credit>
    {
        public CreditRepository(MainContext context):base(context){}
    }
}
