using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Stop automatically cross-fade layers. ")]
	public class EverLoop_StopAutopilot : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
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
			
			theScript.StopAutopilot();
		}

	}
}