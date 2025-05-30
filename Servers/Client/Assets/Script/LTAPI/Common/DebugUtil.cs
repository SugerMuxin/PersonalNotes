namespace LTNet
{
	using UnityEngine;

	//by Kevin
	public class DebugUtil{
		private static bool mIsDebugOn = true;
		private static bool mIsVerboseOn = true;

		public static void SetDebugMode(bool isOn)
		{
			mIsDebugOn = isOn;
		}

		public static void SetVerboseOn(bool isVerboseOn)
		{
			mIsVerboseOn = isVerboseOn;
		}

		public static void LogError(object message)
		{
			if (mIsDebugOn)
				Debug.LogError(message);
		}

		public static void LogWarning(object message)
		{
			if (mIsDebugOn)
				Debug.LogWarning(message);
		}

		public static void Log(object message)
		{
			if (mIsDebugOn && mIsVerboseOn)
			{
				Debug.Log(message);
			}
		}
	}
}