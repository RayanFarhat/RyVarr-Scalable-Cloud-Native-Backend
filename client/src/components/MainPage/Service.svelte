<script lang="ts">
    export let title : string;
    export let desc : string;
    export let imgPaths : string[];

</script>
    <div class="card shadow-xl bg-base-200 m-4">
        <div class="flex justify-around p-4 h-auto">
            {#each imgPaths as path (path)}
            <figure><img class="w-16" src={path} alt="service" /></figure>
          {/each}
        </div>
        <div class="card-body">
        <h2 class="card-title text-transparent bg-clip-text bg-gradient-to-r from-primary to-white">{title}</h2>
          <p>{desc}</p>
        </div>
      </div>

