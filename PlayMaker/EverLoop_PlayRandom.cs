using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Play a number of random layers. ")]
	public class EverLoop_PlayRandom : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public FsmInt numTracks;
		
		public FsmFloat fadeInDuration;
		
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			numTracks = null;
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
			
			theScript.PlayRandom(numTracks.Value, fadeInDuration.Value, ignoreTimeScale.Value);
			
		}

	}
}