namespace Content.Features.PlayerInput.Scripts
{
    public struct PlayerMoveLeftInputEvent { }
    public struct PlayerMoveLeftInputReleasedEvent { }

    public struct PlayerMoveRightInputEvent { }
    public struct PlayerMoveRightInputReleasedEvent { }

    public struct PlayerJumpInputEvent { } // Jump — моментальный, без Released
    public struct PlayerShootInputEvent { }
    public struct PlayerShootInputReleasedEvent { }
}