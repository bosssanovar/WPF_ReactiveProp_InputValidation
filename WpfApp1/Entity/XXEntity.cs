namespace Entity
{
    public class XXEntity : EntityBase
    {
        private const string Text_InitValue = "Init Value";
        private const int Number_InitValue = 100;

        public TextVO Text { get; set; }

        public NumberVO Number { get; set; }

        public XXEntity()
        {
            Text = new TextVO(Text_InitValue);
            Number = new NumberVO(Number_InitValue);
        }

        public void Init()
        {
            Text = new TextVO(Text_InitValue);
            Number = new NumberVO(Number_InitValue);
        }
    }
}
