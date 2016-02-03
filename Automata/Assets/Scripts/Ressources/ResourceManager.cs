using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {
	private static List<Resource> ms_Resources = new List<Resource>();
	private static int ms_level = 0;

	public static void addResource(Resource res) {
		res.level = ms_level;
		ms_Resources.Add(res);

		ms_level++;
	}

	public static Resource levelResource(int level) {
		return ms_Resources[level];
	}
}
