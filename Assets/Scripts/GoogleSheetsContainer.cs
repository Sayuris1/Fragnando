using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;

public class PersonalitySheet : Sheet<PersonalitySheet.Row>
{
    public class Row : SheetRow
    {
        public string Text { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double Altitude { get; private set; }

        public float FontSize{ get; private set; }
        public float Scale { get; private set; }
    }
}

public class SheetContainer : SheetContainerBase
{
    public SheetContainer() : base(UnityLogger.Default) {}

    public PersonalitySheet PersonalitySheet { get; private set; }
}