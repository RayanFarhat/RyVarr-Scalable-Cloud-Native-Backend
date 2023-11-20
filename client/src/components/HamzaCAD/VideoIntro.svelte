<script>
  let source = "/hamzacad/hamzacad_text.mp4";
</script>

<div class="hero bg-base-200 mt-20">
  <div class="hero-content flex-col-reverse lg:flex-row-reverse">
    <!-- first col -->
    <video width="640" height="360" autoplay loop muted>
      <source src="/hamzacad/hamzacad_first_withtext.mp4" type="video/mp4" />
      <!-- <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions"> -->
    </video>
    <!-- second col -->
    <video width="640" height="360" autoplay loop muted>
      <source src="/hamzacad/without_hamzacad_withtext.mp4" type="video/mp4" />
      <!-- <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions"> -->
    </video>
  </div>
</div>

<div class="flex items-center flex-col">
  <a class="p-0 my-20 hover:bg-transparentt" href="/pricing">
    <button class="btn btn-primary">{"Subscribe and Download"}</button></a
  >
  <ul class="menu menu-horizontal bg-base-200 rounded-box">
    <li>
      <button on:click={() => (source = "/hamzacad/hamzacad_text.mp4")}>
        Change text structure as you like</button
      >
    </li>
    <li>
      <button on:click={() => (source = "/hamzacad/hamzacad_lang.mp4")}>
        Supporting ENG/HEB languages
      </button>
    </li>
  </ul>
  {#key source}
    <!-- muted to work more correctly -->
    <video width="854" height="480" autoplay loop muted>
      <source src={source} type="video/mp4" />
      <!-- <track src="captions_en.vtt" kind="captions" srclang="en" label="english_captions"> -->
    </video>
  {/key}
</div>
