using System;

public static class EventManager
{
    public static event Action<string> OnScreenChange = delegate { };

    public const string GET_STARTED = "GET_STARTED";
    public const string HOME_PAGE = "HOME_PAGE";
    public const string PROFILE_PAGE = "PROFILE_PAGE";
    public const string CAMERA_PAGE = "CAMERA_PAGE";
    public const string MAKE_CHOICE_PAGE = "MAKE_CHOICE_PAGE";
    public const string CHOOSE_CORRECT_PAGE = "CHOOSE_CORRECT_PAGE";
    public const string WRONG_CHOICE_PAGE = "WRONG_CHOICE_PAGE";
    public const string CORRECT_CHOICE_PAGE = "CORRECT_CHOICE_PAGE";
    public const string LEARN_PAGE = "LEARN_PAGE";
    public const string ABOUT_PAGE = "ABOUT_PAGE";
    public static void ChangeScreen(string screenName)
    {
        OnScreenChange.Invoke(screenName);
    }
}