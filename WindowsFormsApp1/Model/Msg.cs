namespace WindowsFormsApp1.Model
{
    public class Msg
    {
        public string content;
        public string picUrl;

        public Msg(string content, string picUrl)
        {
            this.content = content;
            this.picUrl = picUrl;
        }        
        public Msg(string content)
        {
            this.content = content;
        }
    }
}