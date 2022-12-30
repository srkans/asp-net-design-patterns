namespace webappStrategy.Models
{
    public class Settings
    {
        public static string claimDbType = "databasetype";

        public EDataBaseType DataBaseType;

        public EDataBaseType getDefaultDbType => EDataBaseType.SqlServer; //sadece set olan bir prop 
    }
}
