
namespace Abstract
{
    public interface IState
    {
        void OnSetup();
        
        void OnEnter();
        
        void OnExit();
    }
}