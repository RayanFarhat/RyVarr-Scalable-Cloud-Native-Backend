<script>
	let source="/hamzacad/hamzacad_text.mp4";
</script>

<div class="diff aspect-[16/9] mt-20 lg:p-16">
    <div class="diff-item-1">
            <video width="1280" height="720" autoplay loop muted>
                <source src="/hamzacad/hamzacad_first_withtext.mp4" type="video/mp4">
                <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions">
            </video>
    </div>
    <div class="diff-item-2">
            <video width="1280" height="720" autoplay loop muted>
                <source src="/hamzacad/without_hamzacad_withtext.mp4" type="video/mp4">
                <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions">
            </video>
    </div>
    <div class="diff-resizer"></div>
  </div>

<div class="flex items-center flex-col mt-20">
    <ul class="menu menu-horizontal bg-base-200 rounded-box">
        <li><button
            on:click={() => source ="/hamzacad/hamzacad_text.mp4"}
            >
            Change text structure as you like</button></li>
        <li><button
            on:click={() => source ="/hamzacad/hamzacad_lang.mp4"}
            >
            Supporting multiple languages</button></li>
      </ul>
{#key source}
<!-- muted to work more correctly -->
<video width="1280" height="720" autoplay loop muted>
    <source src={source} type="video/mp4">
    <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions">
</video>
{/key}
</div>