using System;

public class CounterState
{
    // _currentCount holds the current counter value
    // for the entire application
    private int _currentCount = 0;
    // StateChanged is an event handler other pages
    // can subscribe to 
    public event EventHandler StateChanged;
    // This method will always return the current count
    public int GetCurrentCount()
    {
        return _currentCount;
    }
    // This method will be called to update the current count
    public void SetCurrentCount(int paramCount)
    {
        _currentCount = paramCount;
        StateHasChanged();
    }
    // This method will allow us to reset the current count
    public void ResetCurrentCount()
    {
        _currentCount = 0;
        StateHasChanged();
    }

    private void StateHasChanged()
    {
        // This will update any subscribers
        // that the counter state has changed
        // so they can update themselves
        // and show the current counter value
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}