namespace ChatTwo.Code;

/// <summary>
/// Expected from a newer <c>Dalamud.Game.Chat</c> API. That type is not in Dalamud API 13,
/// so it is defined locally with the same members ChatSource already shifts by.
/// </summary>
/// <remarks>
/// <c>IChatGui.ChatMessageUnhandled</c> on this API is
/// <c>(XivChatType type, int timestamp, SeString sender, SeString message)</c> and carries no
/// relation. Incoming messages therefore report <see cref="LocalPlayer"/> for source and target,
/// and chat-source filtering that depends on this enum does not distinguish speakers.
/// </remarks>
public enum XivChatRelationKind : byte
{
    LocalPlayer = 0,
    PartyMember = 1,
    AllianceMember = 2,
    OtherPlayer = 3,
    EngagedEnemy = 4,
    UnengagedEnemy = 5,
    FriendlyNpc = 6,
    PetOrCompanion = 7,
    PetOrCompanionParty = 8,
    PetOrCompanionAlliance = 9,
    PetOrCompanionOther = 10,
}
