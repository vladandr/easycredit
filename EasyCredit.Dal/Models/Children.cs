namespace EasyCredit.DAL.Models
{
    public class Children: EasyCreditEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public int DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
