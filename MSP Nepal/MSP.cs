using SQLite.Net.Attributes;

namespace MSP_Nepal
{
    internal class MSP
    {

        public string FullName {
            get; set;
        }

        public string College
        {
            get; set;
        }

        public string Bio
        {
            get; set;
        }
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
        public MSP()
        {

        }
        public MSP(string name, string clg, string bio) 
        {
            FullName = name;
            College = clg;
            Bio = bio;
        }
    }
}