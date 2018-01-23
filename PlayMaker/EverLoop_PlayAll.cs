using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Play all layers that are on the same GameObject as this component. ")]
	public class EverLoop_PlayAll : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public enum PlayAll
		{
			no_parameters,
			fadeInDuration_ignoreTimeScale
		}
		
		public PlayAll playAllMethods;

		public FsmFloat fadeInDuration;
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			playAllMethods = PlayAll.no_parameters;
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
			
			switch (playAllMethods)
			{
			case PlayAll.no_parameters:
				theScript.PlayAll();
				break;
			case PlayAll.fadeInDuration_ignoreTimeScale:
				theScript.PlayAll(fadeInDuration.Value, ignoreTimeScale.Value);
				break;
			}
			
		}

	}
}