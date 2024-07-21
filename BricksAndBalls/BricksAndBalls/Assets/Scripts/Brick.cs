using TMPro;
using UnityEngine;
using VContainer;

public class Brick : MonoBehaviour
{
	[SerializeField] private int health;
	[SerializeField] private TMP_Text label;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Color minHealthColor;
	[SerializeField] private Color maxHealthColor;

	private int maxHealth;
	private GamePlayEvents gamePlayEvents;
	private bool isHit;
	
	private const float HitCooldown = 0.1f; 
	private float lastHitTime;

	public int Health => health;

	[Inject]
	public void Constructor(GamePlayEvents gamePlayEvents)
	{
		this.gamePlayEvents = gamePlayEvents;
	}

	public void Start()
	{
		maxHealth = health;
		lastHitTime = -HitCooldown;
		UpdateLabel();
		UpdateColor();
	}
	
	public void SetHealth(int newHealth)
	{
		health = newHealth;
		maxHealth = health;
		UpdateLabel();
		UpdateColor();
	}

	
	private void UpdateLabel()
	{
		if (label != null)
		{
			label.SetText($"{health}");
		}
	}
	
	private void UpdateColor()
	{
		if (spriteRenderer != null)
		{
			float healthPercentage = (float)health / maxHealth;
			spriteRenderer.color = Color.Lerp(minHealthColor, maxHealthColor, healthPercentage);
		}
	}
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && !isHit)
		{
			float currentTime = Time.time;
			if (currentTime - lastHitTime >= HitCooldown)
			{
				lastHitTime = currentTime;
				isHit = true;
				health = Mathf.Clamp(--health, 0, maxHealth);

				gamePlayEvents.OnBrickHit?.Invoke();
				UpdateLabel();
				UpdateColor();

				if (health <= 0)
				{
					gameObject.SetActive(false);
				}
			}
		}
	}
	
	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			isHit = false;
		}
	}
}
