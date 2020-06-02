using JARS.Data.NH.Interfaces;

namespace JARS.BOS.Data
{
    public interface IDataContextBOS : IDataContextBaseNh
    {
        //note that it uses the IDataContextNh interface and not just the IDataContext interface
        //if the ...Nh interface is not used the Context will not be detected by MEF
    }

    public interface IDataContextExternalBOS : IDataContextBaseNh
    {
        //note that it uses the IDataContextNh interface and not just the IDataContext interface
        //if the ...Nh interface is not used the Context will not be detected by MEF
    }
}
