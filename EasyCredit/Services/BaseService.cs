using AutoMapper;


namespace EasyCredit.Services
{
    public abstract class BaseService
    {
        protected static TDest MapTo<TDest>(object obj) => Mapper.Map<TDest>(obj);
    }
}
