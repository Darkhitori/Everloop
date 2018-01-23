using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Stop all currently playing layers. ")]
	public class EverLoop_StopAll : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public enum StopAll
		{
			no_parameters,
			fadeInDuration_ignoreTimeScale
		}
		
		public StopAll stopAllMethods;

		public FsmFloat fadeInDuration;
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			stopAllMethods = StopAll.no_parameters;
			fadeInDuration = null;
			ignoreTimeScale = true;
			everyFrame = true;
		}
       
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			theScript = go.GetComponent<EverloopController>();


			if (!everyFrame.Value)
			{
				DoTheMagic();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoTheMagic();
			}
		}

		void DoTheMagic()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			switch (stopAllMethods)
			{
			case StopAll.no_parameters:
				theScript.StopAll();
				break;
			case StopAll.fadeInDuration_ignoreTimeScale:
				theScript.StopAll(fadeInDuration.Value, ignoreTimeScale.Value);
				break;
			}
			
		}

	}
}