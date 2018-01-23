using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Toggle between on and off of all layers. ")]
	public class EverLoop_PlayStopAll : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public FsmBool fadeInOut;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			fadeInOut = true;
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
			
			theScript.PlayStopAll(fadeInOut.Value);
		}

	}
}