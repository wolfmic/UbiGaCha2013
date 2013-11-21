using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] Playlist;
	public int PlaylistIndex = 0;
	public bool StartAwake = true;
	public bool AutoNext = false;

	private int _prevPositionInMusic;
	private bool _next = false;
	private bool _rewind = false;

	// Use this for initialization
	void Start () {
		if (this.Playlist.Length == 0) {
			return;
		}
		this.Play();
	}
	
	// Update is called once per frame
	void Update () {
		int positionInMusic = this.audio.timeSamples;

		if (this.AutoNext)
			_next = true;
		if (_prevPositionInMusic > positionInMusic && ((_next && this.PlaylistIndex < this.Playlist.Length - 1) || _rewind)) {
			if (_next)
				this.PlaylistIndex = Mathf.Min(this.Playlist.Length - 1, this.PlaylistIndex + 1);
			if (_rewind)
				this.PlaylistIndex = 0;
			this.Play();
			_next = false;
			_rewind = false;
		}
		_prevPositionInMusic = this.audio.timeSamples;
	}

	void Play() {
		this.audio.clip = this.Playlist[this.PlaylistIndex];
		this.audio.Play();
		this.audio.loop = true;
        _prevPositionInMusic = 0;
	}

    public void PlayRightNow(AudioClip clip)
    {
        this.audio.clip = clip;
        this.audio.Play();
        this.audio.loop = true;
        _prevPositionInMusic = 0;
        this.Rewind();
    }

	public void Next() {
		_next = true;
	}

	public void Rewind() {
		_rewind = true;
	}
}
