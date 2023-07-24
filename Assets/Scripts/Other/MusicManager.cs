using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    protected static MusicManager instance;
    public static MusicManager Instance => instance;

    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;
    public AudioClip buttonBuyShop;
    public AudioClip dropSound;
    public AudioClip skillBomb;
    public AudioClip skillX2;
    public AudioClip skillHammer;
    public AudioClip gameOver;

     
    public AudioSource audioSource;
    public AudioSource audioSource1;
    private void Awake()
    {
        // Kiểm tra nếu đã có một instance MusicManager tồn tại thì hủy instance mới và giữ instance cũ
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Lưu instance MusicManager đang hoạt động
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    protected virtual void Start()
    {
        this.PlayBackgroundMusic();
    }
    // Phát âm thanh nền
    public void PlayBackgroundMusic()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void ToggleSound()
    {
        if (!GameManager.Instance.SoundEnabled)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Stop(); // Tắt âm thanh nếu đang phát
        }
        else
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play(); // Bật âm thanh nếu đã tắt
        }
    }

    // Phát âm thanh nút (button click sound)
    public void PlayButtonClickSound()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(buttonClickSound);
    }
    public void PlayButtonBuyShop()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(buttonBuyShop);
    }
    public void PlaySoundHammer()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(skillHammer);
    }
    public void PlaySoundX2()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(skillX2);
    }
    public void PlaySoundBomb()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(skillBomb);
    }    
    public void PlaySoundBySkill()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(buttonBuyShop);
    }    
    public void PlaySoundDrop()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(dropSound);
    }
    public void PlayGameOver()
    {
        if (!GameManager.Instance.SoundEnabled) return;
        audioSource1.PlayOneShot(gameOver);
    }    

}
