import * as BABYLON from 'babylonjs';

const canvas = document.getElementById("renderCanvas") as HTMLCanvasElement;; // Get the canvas element
const engine = new BABYLON.Engine(canvas, true); // Generate the BABYLON 3D engine
const createScene = function () {
  // This creates a basic Babylon Scene object (non-mesh)
  const scene = new BABYLON.Scene(engine);
  // This creates and positions a free camera (non-mesh)
  // Creates, angles, distances and targets the camera
  var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0, 10, new BABYLON.Vector3(0, 0, 0), scene);

  // This positions the camera
  camera.setPosition(new BABYLON.Vector3(0, 6, 10));

  // This attaches the camera to the canvas
  camera.attachControl(canvas, true);  // This targets the camera to scene origin
  camera.setTarget(BABYLON.Vector3.Zero());
  // This attaches the camera to the canvas
  camera.attachControl(canvas, true);
  // This creates a light, aiming 0,1,0 - to the sky (non-mesh)
  const light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(0, 1, 0), scene);
  // Default intensity is 1. Let's dim the light a small amount
  light.intensity = 0.7;
  // Our built-in 'sphere' shape.
  const sphere = BABYLON.MeshBuilder.CreateSphere("sphere", { diameter: 2, segments: 32 }, scene);
  // Move the sphere upward 1/2 its height
  sphere.position.y = 1;
  // Our built-in 'ground' shape.
  const ground = BABYLON.MeshBuilder.CreateGround("ground", { width: 6, height: 6 }, scene);

  var points = [];
  for (var i = 0; i < 50; i++) {
    points.push(new BABYLON.Vector3(i - 25, 5 * Math.sin(i / 2), 0));
  }

  // Path3D
  var path3d = new BABYLON.Path3D(points);
  var curve = path3d.getCurve();
  var tangents = path3d.getTangents();
  var normals = path3d.getNormals();
  var binormals = path3d.getBinormals();

  var li = BABYLON.MeshBuilder.CreateTube('li', { path: curve, radius: 0.5, sideOrientation: BABYLON.Mesh.DOUBLESIDE }, scene);
  return scene;
};
const scene = createScene(); //Call the createScene function
// Register a render loop to repeatedly render the scene
engine.runRenderLoop(function () {
  scene.render();
});
// Watch for browser/canvas resize events
window.addEventListener("resize", function () {
  engine.resize();
});