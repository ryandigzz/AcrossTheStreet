FirstPersonController setup

1) Prepare the Capsule
- Select your Capsule (or create one): GameObject -> 3D Object -> Capsule.
- Add a `CharacterController` component to the capsule.

2) Add the Script
- Place `FirstPersonController.cs` on the capsule (Assets/Scripts/Player).
- Create an empty child transform at eye height: Right-click the capsule -> Create Empty -> name it `CameraHolder`. Set its local Y to ~1.6 (adjust for your character).
- Move your Main Camera under `CameraHolder` (drag it as a child) and reset the camera's local position to (0,0,0).
- In the capsule inspector, assign `CameraHolder` to the `cameraHolder` field on `FirstPersonController`.

3) Input bindings
- This script uses Unity's legacy input axes: `Horizontal`, `Vertical`, `Mouse X`, `Mouse Y`, and `Jump`.
- If you're using the new Input System, either enable "Send Unity Events"/compatibility or replace input calls with your input action callbacks.

4) Cinemachine hookup (optional, smoothing / blends)
- Install Cinemachine: Window -> Package Manager -> search "Cinemachine" -> Install.
- Create a Virtual Camera: GameObject -> Cinemachine -> Create Virtual Camera.
- In the Virtual Camera inspector:
  - Set `Follow` to the `CameraHolder` transform (drag the CameraHolder from the Hierarchy).
  - Set `Body` to `Hard Lock To Target` (so the vcam matches the `CameraHolder` position exactly).
  - Set `Aim` to `Do Nothing` (we let the player script control camera rotation).
- The main camera should have a `CinemachineBrain` (added automatically). The Virtual Camera will drive the main camera's transform to match `CameraHolder`.

Notes & tips
- For true first-person control, keep pitch (vertical rotation) driven by the `cameraHolder` and yaw by the player body — this script follows that pattern.
- If you prefer Cinemachine to handle all rotation, change the Virtual Camera's Aim to a suitable composer and remove the script's camera rotation code.
- Tweak `walkSpeed`, `runSpeed`, `mouseSensitivity`, and `jumpHeight` in the inspector for desired feel.

If you want, I can also add a small editor helper to auto-create `CameraHolder` and wire the script.
