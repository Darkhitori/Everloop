using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Everloop;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("EverLoop")]
	[Tooltip("Linearly fade in a specific layer. ")]
	public class EverLoop_FadeInLayer : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(EverloopController))] 
		public FsmOwnerDefault gameObject;
		
		[ObjectType(typeof(AudioSource))]
		public FsmObject layer;
		
		public FsmFloat duration;
		
		public FsmBool ignoreTimeScale;
		
		public FsmBool everyFrame;

		EverloopController theScript;

		public override void Reset()
		{
			gameObject = null;
			layer = null;
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
			
			var fLayer = layer.Value as AudioSource;
			if (fLayer == null)
			{
				return;
			}
			
			theScript.FadeInLayer(fLayer, duration.Value, ignoreTimeScale.Value);
			
		}

	}
}