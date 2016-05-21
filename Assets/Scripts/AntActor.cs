using UnityEngine;
using System;

namespace Anthill.Animation
{
	[RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("Anthill/Actor")]
	public class AntActor : MonoBehaviour
	{
		[System.Serializable]
		public struct AntAnimation
		{
			public string name;
			public Sprite[] frames;
		}

		public string initialAnimation;
		public AntAnimation[] animations;
		public float timeScale = 1f;
		public bool reverse = false;
		public bool loop = true;

		public Action<string> OnAnimationComplete;

		private SpriteRenderer _sprite;
		private AntAnimation _currentAnimation;
		private float animationSpeed = 29f;
		private float _currentFrame;
		private bool _playing;
		private int _prevFrame;

		void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
			_currentFrame = 1f;
			_playing = true;
			_prevFrame = -1;

			if (initialAnimation != null)
			{
				SwitchAnimation(initialAnimation);
			}
		}

		void Update()
		{
			if (_playing)
			{
				if (reverse)
				{
					if (loop && RoundFrame(_currentFrame) <= 0)
						_currentFrame = TotalFrames;
					PrevFrame(true);
					if (loop && RoundFrame(_currentFrame) <= 0)
					{
						_currentFrame = TotalFrames;
						if (OnAnimationComplete != null)
							OnAnimationComplete.Invoke(_currentAnimation.name);
					}
				}
				else
				{
					if (loop && RoundFrame(_currentFrame) >= TotalFrames - 1)
						_currentFrame = 1f;
					NextFrame(true);
					if (loop && RoundFrame(_currentFrame) >= TotalFrames - 1)
					{
						_currentFrame = 1f;
						if (OnAnimationComplete != null)
							OnAnimationComplete.Invoke(_currentAnimation.name);
					}
				}
			}
		}

		public void SwitchAnimation(string aAnimationName)
		{
			if (aAnimationName != _currentAnimation.name)
			{
				bool animationFound = false;
				for (int i = 0; i < animations.Length; i++)
				{
					if (animations [i].name == aAnimationName)
					{
						animationFound = true;
						_currentAnimation = animations [i];
						_currentFrame = 1.0f;
						break;
					}
				}

				if (!animationFound)
					Debug.LogWarning ("Can't find animation \"" + aAnimationName + "\".");
			}
		}

		public void Play()
		{
			_playing = true;
		}

		public void Stop()
		{
			_playing = false;
		}

		public void GotoAndStop(float aFrame)
		{
			_currentFrame = (aFrame <= 0f) ? 1f : (aFrame > TotalFrames) ? TotalFrames : aFrame;
			SetFrame(_currentFrame);
			Stop();
		}

		public void GotoAndPlay(float aFrame)
		{
			_currentFrame = (aFrame <= 0f) ? 1f : (aFrame > TotalFrames) ? TotalFrames : aFrame;
			SetFrame(_currentFrame);
			Play();
		}

		public void PlayRandomFrame()
		{
			GotoAndPlay(UnityEngine.Random.Range(1, TotalFrames));
		}

		public void NextFrame(bool aUseSpeed = false)
		{
			if (aUseSpeed)
				_currentFrame += (animationSpeed * Time.deltaTime) * timeScale;
			else
				_currentFrame += 1f;
			SetFrame(_currentFrame);
		}

		public void PrevFrame(bool aUseSpeed = false)
		{
			if (aUseSpeed)
				_currentFrame -= (animationSpeed * Time.deltaTime) * timeScale;
			else
				_currentFrame -= 1f;
			SetFrame(_currentFrame);
		}

		protected void SetFrame(float aFrame)
		{
			if (_currentAnimation.frames != null)
			{
				int i = RoundFrame(aFrame);
				if (_prevFrame != i)
				{
					_sprite.sprite = _currentAnimation.frames[i];
					_prevFrame = i;
				}
			}
		}

		protected int RoundFrame(float aFrame)
		{
			float i = Mathf.Floor(aFrame);
			return Mathf.RoundToInt((i <= 0) ? 0 : (i >= TotalFrames - 1) ? TotalFrames - 1 : i);
		}

		public bool IsPlaying
		{
			get { return _playing; }
		}

		public string AnimationName
		{
			get { return _currentAnimation.name; }
			set { SwitchAnimation(value); }
		}

		public int TotalFrames
		{
			get { return (_currentAnimation.frames != null) ? _currentAnimation.frames.Length : 0; }
		}

		public int CurrentFrame
		{
			get { return RoundFrame(_currentFrame); }
		}

	}
}