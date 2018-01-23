using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Gradually decrease volume of all currently playing layers. ")]
	public class EverLoop_FadeOutAll : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		public FsmFloat duration;
		
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			duration = 1f;
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
			
			theScript.FadeOutAll(duration.Value, ignoreTimeScale.Value);
			
		}

	}
}