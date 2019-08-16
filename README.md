# COMP313 Prototype

Game Loop:

    The game loop and all of the inputs are currently handled in the player movement field. At the moment it accepts the following user inputs: WASD or the arrow keys for movement, Space for jump
    and CTRL or left mouse button for attack. 


Game Flow:

    The game flow is very similar to most beat'em'up games with the level design being very similar although in this prototytpe it is a bit smaller. To progress the player must defeat 
    all of the enemys that are alive at the current stage before they can move forward in the level. This repe3ats until the player is at the end of the level. Once they have completed 
    the level the plan is to have a sort of world map in which the player can either choose to progress to the next level or repeat a previous level. In the prototype the player progresses 
    through a small level with three stages with the number of enemies in each stage. As they defeat enemys they gain experience allowing them to level to level up. In the future there will
    be a feature to allow them to use these levels to improve their character statistics.

Asset store Links:

    PSD logo templates - https://assetstore.unity.com/packages/2d/gui/icons/psd-logo-templates-103928 
    Stylized cartoony sandwich - https://assetstore.unity.com/packages/3d/props/food/stylized-cartoony-sandwich-102301
    standard assets - https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351
    polydesert - https://assetstore.unity.com/packages/3d/environments/landscapes/polydesert-107196
    ground materials fd free - https://assetstore.unity.com/packages/2d/textures-materials/floors/ground-materials-fd-free-140364
    Knight sprite sheet free - https://assetstore.unity.com/packages/2d/characters/knight-sprite-sheet-free-93897

Controls: 

    Movement:
    
        W / Arrow Key Up  - Move Up
        S / Arrow Key Down  - Move Down
        A / Arrow Key Left  - Move Left
        D / Arrow Key Right  - Move Right
        Space - Jump
    
    Combat:
    
        CTRL / Left Mouse Button - Attack
        Space + CTRL / Left Mouse Button - Slam Attack
        
The Process and the main challenges:
    
    During the proces the first big challenge after deciding what the game was going tro be and what we wanted the core mechanics to be like was deciding how to create the level. 
    Initially we looked at using a 2D scene and either a flat surface along the x axis or to use the y axis as part of the ground however this removed the ability to add a way for the player
    jump. Eventually we deciding on building our level in a 3D space and making it a 2.5D style game. This presented some new challenges when it cam eto the level design as we had to
    figure out how to not only use the 3d objects to create a scene that looked right and made it easy to see where the palyer could move but it also meant we had to take a very different approach with design.
    
    Getting the player to move up and down the as well as left to right was not challenging but getting the enemy to follow the player along not only the x axis but the z axis as well was a bit more difficult.
    Initially I tried to do it through code but that did not work too well. Eventually I moved to using a nav mesh to have to enemy locate the player and follow them. Once this was done getting the animations to
    work when required was mostly pretty straighforward with the slam animation being only one I wasn't able to get to trigger. After this figuring out a way to get the player to attack presented some issues 
    however by adding the sphere collider as a trigger it made it a lot easier to get the enemies attacking at the right time. This didn't work with the player attacking and I ended up having to 
    learn how to use raycasts in order for the playable character to find if an enemy is in range. While these worked  it was not great as both the player and the enemys could attack constantly so I
    ended up using a timer to stop this however, I hindsight a coroutine would have been a better way to do this.
    
    After getitng all of this working the developmment of the health system of both the player and the enemies as well as health pickups was poretty simple. Linking the health system to the UI
    for the player was also quite simple along with the addition of an exp bar and having both update. The next challange came when implementing the wave system, this was an interesting one as 
    it was quite difficult to get going at first and the method used was quite rough and will need a fair bit of refinement if this game progresses as it own't be efficient to hard code every enemy in 
    each level. THe final big challenge was in getting the slam attack to work. This was difficult as it took a fair bit of time to figure out why the enemies werent moving when the action was performed.
    htis came down to the isKinematic feature in their rigibody being set to true. Once disabling this I had to mess around with their mass and drag as they would act like they were on ice when the force was
    applied. Jumping also became a problem during this as I didn't have a good way of checking it the player was on the ground. For this I also had to use raycasts to see how far from the ground the player was.
    
    Some features that we were hoping to add didn't end up making it in due to both time constraints and not being able to figure out how to use them. These features that would have a big impact on how the
    player interacts with the game are a block feature, a stun featue (especially for the slam) and the implementation of items and collectable weapons.
