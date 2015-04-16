using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	#region Members
	public AudioClip Damage ;
	public AudioClip Explosion;
	public AudioClip Fight ;
	public AudioClip Fire_Start ;
	public AudioClip Fire_Loop ;
	public AudioClip Gaz ;
	public AudioClip Oreebleue_A ;
	public AudioClip Oreebleue_T ;
	public AudioClip Player_Potion ;
	public AudioClip Player_Interraction ;
	public AudioClip Potion ;
	public AudioClip Spider_Die ;
	public AudioClip Spider_Split ;
	public AudioClip Spider_Bloque;
	public AudioClip Quest_panacee;
	public AudioClip Quest_ingredient;
	public AudioClip Quest_all;
	
	public AudioClip Player_Drop;
	public AudioClip Player_Pick;

	public AudioSource camera;
	public AudioSource camera_ui;
	public AudioSource player;
	public AudioSource potion;
	public AudioSource spider;
	public AudioSource other;
	public AudioSource explosion;
	public AudioSource quest;

	#endregion

	// Use this for initialization
	void Start()
	{
		SoundManagerEvent.onEvent += Effect;
		camera.Stop();
		camera.clip = Oreebleue_A;
		camera.Play();
		camera_ui.Stop();
		camera_ui.clip = Oreebleue_T;
		camera_ui.Play();


	}

	void OnDestroy()
	{
		SoundManagerEvent.onEvent -= Effect;
	}

	void Effect(SoundManagerType emt)
	{
		switch (emt)
		{
			case SoundManagerType.DAMAGE:
				player.Stop();
				player.clip = Damage;
				player.Play();
				break;
			case SoundManagerType.EXPLOSION:
				explosion.Stop();
				explosion.clip = Explosion;
				explosion.Play();
				break;
			case SoundManagerType.FIGHT:
				camera_ui.Stop();
				camera.Stop();
				camera.clip = Fight;
				camera.Play();
				break;
			case SoundManagerType.FIRE_START:
				other.Stop();
				other.clip = Fire_Start;
				other.Play();
				break;
			case SoundManagerType.FIRE_LOOP:
				other.Stop();
				other.clip = Fire_Loop;
				other.Play();
				break;
			case SoundManagerType.GAZ:
				other.Stop();
				other.clip = Gaz;
				other.Play();
				break;
			case SoundManagerType.OREEBLEUE:
				camera.Stop();
				camera.clip = Oreebleue_A;
				camera.Play();
				camera_ui.Stop();
				camera_ui.clip = Oreebleue_T;
				camera_ui.Play();
				break;
			case SoundManagerType.PLAYER_POTION:
				potion.Stop();
				potion.clip = Player_Potion;
				potion.Play();
				break;
			case SoundManagerType.PLAYER_INTERRACTION:
				player.Stop();
				player.clip = Player_Interraction;
				player.Play();
				break;
			case SoundManagerType.PLAYER_DROP:
				player.Stop();
				player.clip = Player_Drop;
				player.Play();
				break;
			case SoundManagerType.PLAYER_PICK:
				player.Stop();
				player.clip = Player_Pick;
				player.Play();
				break;
			case SoundManagerType.POTION:
				potion.Stop();
				potion.clip =Potion;
				potion.Play();
				break;
			case SoundManagerType.SPIDER_DIE:
				spider.Stop();
				spider.clip = Spider_Die;
				spider.Play();
				break;
			case SoundManagerType.SPIDER_SPLIT:
				spider.Stop();
				spider.clip = Spider_Split;
				spider.Play();
				break;
			case SoundManagerType.SPIDER_BLOQUE:
				spider.Stop();
				spider.clip = Spider_Bloque;
				spider.Play();
				break;
			case SoundManagerType.QUEST_ALL:
				quest.Stop();
				quest.clip = Quest_all;
				quest.Play();
				break;
			case SoundManagerType.QUEST_INGREDIENT:
				quest.Stop();
				quest.clip = Quest_ingredient;
				quest.Play();
				break;
			case SoundManagerType.QUEST_PANACEE:
				quest.Stop();
				quest.clip = Quest_panacee;
				quest.Play();
				break;
			
		}
	}

	

}
