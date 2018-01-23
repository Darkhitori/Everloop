using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Automatically cross-fade between different layers. Autopilot uses properties of this component. ")]
	public class EverLoop_StartAutopilot : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public enum StartAutopilot
		{
			no_parameters,
			fadeInDuration_ignoreTimeScale
		}
		
		public StartAutopilot startAutopilotMethods;
		
		public FsmInt numInitTracks;
		public FsmFloat fadeInDuration;
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			startAutopilotMethods =  StartAutopilot.no_parameters;
			numInitTracks = null;
			fadeInDuration = 3f;
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
			
			switch (startAutopilotMethods)
			{
			case StartAutopilot.no_parameters:
				theScript.StartAutopilot();
				break;
			case StartAutopilot.fadeInDuration_ignoreTimeScale:
				theScript.StartAutopilot(numInitTracks.Value, fadeInDuration.Value, ignoreTimeScale.Value);
				break;
			}
			
		}

	}
}