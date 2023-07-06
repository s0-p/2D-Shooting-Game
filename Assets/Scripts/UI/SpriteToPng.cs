using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToPng : MonoBehaviour
{
    [SerializeField]
    UITexture m_texture;
    Sprite[] m_sprites;
    // Start is called before the first frame update
    void Start()
    {
        string path = null;
#if UNITY_EDITOR
        path = Application.dataPath;
#else
        path = Application.persistentDataPath;
#endif
        m_sprites = Resources.LoadAll<Sprite>("Fonts");
        for (int i = 0; i < m_sprites.Length; i++)
        {
            var spr = m_sprites[i];
            Texture2D texture = new Texture2D((int)spr.rect.width, (int)spr.rect.height, TextureFormat.ARGB32, false);
            for (int y = 0; y < spr.rect.height; y++)
            {
                for (int x = 0; x < spr.rect.width; x++)
                {
                    texture.SetPixel(x, y, spr.texture.GetPixel((int)spr.rect.x + x, (int)spr.rect.y + y));
                }
            }
            texture.Apply();
            var bytes = texture.EncodeToPNG();
            File.WriteAllBytes(string.Format("{0}/Fonts/imageFont_{1:00}.png", path, i + 1), bytes);
            m_texture.mainTexture = texture;
            m_texture.MakePixelPerfect();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
