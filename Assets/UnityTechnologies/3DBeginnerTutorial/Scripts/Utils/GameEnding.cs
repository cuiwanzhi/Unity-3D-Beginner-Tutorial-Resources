#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// 游戏结束
/// </summary>
public class GameEnding : MonoBehaviour {

	//============================public属性============================//

	/// <summary>
	/// 游戏胜利的时候，展示的内容
	/// </summary>
	public GameObject endScene;

	/// <summary>
	/// 失败展示内容
	/// </summary>
	public GameObject failScene;

	/// <summary>
	/// 胜利的音效
	/// </summary>
	public AudioSource winAudio;

	/// <summary>
	/// 失败的音效
	/// </summary>
	public AudioSource failAudio;

	//============================private属性============================//
	
	/// <summary>
	/// 结束范围触发器
	/// </summary>
	private Rigidbody _endGameScope;

	/// <summary>
	/// 玩家胜利
	/// </summary>
	private bool _isPlayerWin = false;

	/// <summary>
	/// 玩家失败
	/// </summary>
	private bool _isPlayerFail = false;


	static private GameEnding _instance;
	private GameObject _player;

	static public GameEnding instance() {
		if (GameEnding._instance) return GameEnding._instance;
		GameEnding._instance = FindObjectOfType<GameEnding>();
		return GameEnding._instance;
	}


	// Start is called before the first frame update
	void Start() {
		this._endGameScope = this.GetComponent<Rigidbody>();
		if (this.endScene) this.endScene.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (this._isPlayerWin && Input.anyKeyDown) {
			this.ExitGame();
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && this.endScene) {
			this.OnWin();
		}
	}

	/// <summary>
	/// 结束关卡
	/// </summary>
	void OnWin() {
		if (this._isPlayerWin) return;

		this.winAudio.Play();
		this._isPlayerWin = true;
		this.endScene.SetActive(true);
		// 延时执行推出游戏
		Animator animator = this.endScene.GetComponent<Animator>();
		animator.Play("FadeIn");
	}

	void ExitGame() {
		// 需要判断是不是在编辑器中运行
		if (Application.isEditor)
			EditorApplication.isPlaying = false;
		else
			Application.Quit();
		// Application.Quit();
	}

	/// <summary>
	/// 失败
	/// </summary>
	public void OnFail(GameObject player) {
		// 如果已经胜利或者失败，就不再执行
		if (this._isPlayerFail || this._isPlayerWin) return;
		if (!this.failScene) return;

		this._isPlayerFail = true;
		this._player = player;
		this.failAudio.Play();
		this.failScene.SetActive(true);
		Animator animator = this.failScene.GetComponent<Animator>();
		animator.Play("FadeIn");
		this.Invoke("ReStart", 3);
	}

	/// <summary>
	/// 重新开始
	/// </summary>
	void ReStart() {
		this._player.GetComponent<playerMovement>().ReStart();
		Animator animator = this.failScene.GetComponent<Animator>();
		animator.Play("FadeOut");
		this.StartCoroutine(Delay.Do(() => {
			this.failScene.SetActive(false);
		}, animator.GetCurrentAnimatorStateInfo(0).length));
		this._isPlayerFail = false;
	}
}
