using UnityEngine;

public class AccelRx : MonoBehaviour
{
    [Range(0, 1)]
    public float suddeness = .1f;

    // Start is called before the first frame update
    SerialIO sp;
    Vector3 pos;

    void Start()
    {
        sp = GetComponent<SerialIO>();
    }

    // Update is called once per frame
    void Update()
    {
        string line = sp.Read();

        if (line != "")
        {
            string[] vals = line.Split();

            pos.x = -float.Parse(vals[0]);
            pos.y = float.Parse(vals[2]);
            pos.z = -float.Parse(vals[1]);

            //transform.position = pos;
            transform.position = Vector3.Lerp(transform.position, pos, suddeness);
        }
    }
}
