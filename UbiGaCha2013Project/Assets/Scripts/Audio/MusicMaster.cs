using UnityEngine;
using System.Collections.Generic;

public class MusicMaster : MonoBehaviour {

	public float ducking = 0.5f;

	private GameObject[] _duckableEmitters;
	private float[] _duckableEmittersOriginalVolume;
	private float _unduckingTimer = 0f;
	private float _duckingPeak = 0f;
	private int _isDucking = 0;

	// Use this for initialization
	void Start () {
		_duckableEmitters = GameObject.FindGameObjectsWithTag("DukableSound");
	}
	
	// Update is called once per frame
	void Update () {
		this.ducking = 0.5f;
		if (_unduckingTimer > 0f) {
			// update ducking timers
			_unduckingTimer -= Time.deltaTime;
			// 
			if (_unduckingTimer > 2f * _duckingPeak)
				_isDucking = 0;
			else if (_unduckingTimer > _duckingPeak)
				_isDucking = 1;
			else
				_isDucking = 2;
			// update ducking;
			if (_isDucking == 0)
				SetDucking(1f - (Time.deltaTime / _duckingPeak) * this.ducking);
			else if (_isDucking == 1)
				SetDucking(1f + (Time.deltaTime / _duckingPeak) * this.ducking);
			// pass if when ducking timer elapsed
			if (_unduckingTimer <= 0f) {
				this.TerminateDucking();
				_unduckingTimer = 0f;
				_duckingPeak = 0f;
			}
		}
	}

	public void PlayDucking(AudioSource source) {
		if (_unduckingTimer == 0f) {
			this.PrepareDucking();
		}
		_unduckingTimer = Mathf.Max(source.clip.length, _unduckingTimer);
		_duckingPeak = _unduckingTimer / 3f;
		source.Play();
	}

	void SetDucking(float mult) {
		for (int i = 0; i < _duckableEmitters.Length; ++i) {
			_duckableEmitters[i].audio.volume = Mathf.Max(_duckableEmitters[i].audio.volume * mult, _duckableEmittersOriginalVolume[i] * this.ducking);
		}
	}

	void PrepareDucking() {
		_duckableEmittersOriginalVolume = new float[_duckableEmitters.Length];

		for (int i = 0; i < _duckableEmittersOriginalVolume.Length; ++i) {
			_duckableEmittersOriginalVolume[i] = _duckableEmitters[i].audio.volume;
		}
	}

	void TerminateDucking()
	{
		for (int i = 0; i < _duckableEmittersOriginalVolume.Length; ++i) {
			_duckableEmitters[i].audio.volume = _duckableEmittersOriginalVolume[i];
		}
	}
}
